using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nuova_cartella.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Nuova_cartella.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbContext _db;

        public HomeController(ILogger<HomeController> logger, dbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public List<Purchase> Carrelli
        {
            get
            {
                var carrelliString = HttpContext.Session.GetString("Carrelli");//inizializziamo una sessione http per settare i prodotti che vogliamo
                return carrelliString != null ? JsonConvert.DeserializeObject<List<Purchase>>(carrelliString) : new List<Purchase>();//accedere ai valori dei prodotti che sono nel carrello
                //DeserializeObject serve per prendere una stringa json e deserializzarla in un elenco di tipo Purchase in c#
            }
            set
            {
                HttpContext.Session.SetString("Carrelli", JsonConvert.SerializeObject(value));//memorizza gli oggetti nel carrello
                //Convertiamo l'oggetto del carrello in una stringa json serializzata
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost] // Gestione dati
        public IActionResult Riepilogo(Registrazione registrazione)
        {
            _db.Registrazioni.Add(registrazione);//prendi la registrazione fatta dall'utente e la salvi nel db
            _db.SaveChanges();//salvataggio nel db
            return View(_db.Registrazioni.ToList());
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {

            var corrispondenzaTrovata = false;//settiamo la variabile corrispondenza trovata in false
            var utenteRegistrato = _db.Registrazioni.ToList();//alla variabile utenteRegistrato assegniamo i valori delle registrazioni fatte nel SignUp
            foreach (var item in utenteRegistrato)//facciamo un ciclo per scorrere tutti gli utenti che si sono registrati nel SignUp
            {
                //controlliamo se le variabili che mettiamo nel login ci sono anche nel db 
                if ( item.email == login.Email && item.password == login.Password)
                {
                    //se la condizione viene soddisfatta allora controlliamo l'id, se è uguale a 1 sei l'admin e hai accesso a tutto sennò solo alle funzioni base
                    if (item.RegistrazioneId == 1)
                    {
                        HttpContext.Session.SetString("CanAccessPurchasePage", "true");
                        HttpContext.Session.SetString("CanAccessPurchasePage2", "true");
                        HttpContext.Session.SetString("Nascondi", "true");
                        HttpContext.Session.SetString("Nascondi2", "true");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Nascondi", "true");
                        HttpContext.Session.SetString("Nascondi2", "true");
                    }

                    corrispondenzaTrovata = true;//alla fine del ciclo la variabile da false diventa true e finisce il controllo
                    break;
                }
            }

            if (corrispondenzaTrovata) return View("Index");//se tu eri già registrato nel SignUp e fai login ti porta alla Home
            else return View("SignUp");//se non eri già registrato nel SignUp ti porta automaticamente alla pagina SignUp
        }


        [HttpGet] // Mostra la pagina SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Purchase()
        {

            // if ()
            // {
            //     // L'utente non è autenticato, reindirizza alla pagina di login
            //     return RedirectToAction("SignUp", "Home");
            // }
            var prodotti = _db.Purchase.ToList();//assegniamo alla variabile prodotti , i prodotti presenti nel db
            return View(prodotti);
        }

        [HttpPost]
        public IActionResult Purchase(Purchase p)
        {
            return View(p);
        }

       [HttpPost]
        public IActionResult Cart(string prodottoId, int quantitaDesiderata)
        {
            // Recupera il prodotto selezionato dal database in base all'ID
            var selectedProduct = _db.Purchase.FirstOrDefault(p => p.PurchaseId == prodottoId);

            // Verifica se il prodotto selezionato esiste, se la quantità desiderata è maggiore di zero
            // e se la quantità desiderata è minore o uguale alla quantità disponibile del prodotto
            if (selectedProduct != null && quantitaDesiderata > 0 && quantitaDesiderata <= selectedProduct.quantita)
            {
                // Cerca se esiste già un elemento nel carrello con lo stesso ID prodotto
                var existingItem = Carrelli.FirstOrDefault(p => p.PurchaseId == prodottoId);

                // Se esiste già un elemento con lo stesso ID prodotto, aggiorna la quantità dell'elemento
                if (existingItem != null)
                {
                    existingItem.quantita += quantitaDesiderata;
                }
                else
                {
                    // Se non esiste un elemento nel carrello con lo stesso ID prodotto, crea un nuovo oggetto Purchase
                    var carrello = new Purchase
                    {
                        PurchaseId = selectedProduct.PurchaseId,
                        prodotto = selectedProduct.prodotto,
                        quantita = quantitaDesiderata,
                        prezzo = selectedProduct.prezzo,
                        Immagine = selectedProduct.Immagine
                    };

                    // Calcola il prezzo totale per l'elemento del carrello
                    double totaleElemento = Convert.ToDouble(carrello.prezzo * carrello.quantita);
                    carrello.PrezzoTotale = totaleElemento;

                    // Copia la lista dei carrelli e aggiungi il nuovo oggetto Purchase
                    var carrelli = Carrelli;
                    carrelli.Add(carrello);
                    Carrelli = carrelli;
                }
            }

            // Calcola il totale complessivo dei prezzi di tutti gli elementi nel carrello
            double totaleComplessivo = Carrelli.Sum(item => item.PrezzoTotale);
            ViewData["TotaleComplessivo"] = totaleComplessivo;

            // Serializza la lista dei carrelli in formato JSON e salvala nella sessione HTTP
            HttpContext.Session.SetString("Carrelli", JsonConvert.SerializeObject(Carrelli));

            // Ritorna la vista "Cart" passando la lista dei carrelli come modello
            return View("Cart", Carrelli);
        }

        [HttpGet]
        public IActionResult ChiSiamo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
