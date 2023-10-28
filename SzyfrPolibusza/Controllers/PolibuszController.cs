using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SzyfrPolibusza.Models;

namespace SzyfrPolibusza.Controllers
{
    public class PolibuszController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Szyfrowanie()
        {
            return View(new Polibusz());
        }

        [HttpPost]
        public IActionResult Szyfrowanie(Polibusz model)
        {
            if (ModelState.IsValid)
            {
                // Usuń znaki białe i przekształć tekst na małe litery
                string tekst = model.Tekst.Replace(" ", "").ToLower();

                // Inicjalizuj wynik jako pustą listę liczb całkowitych
                List<int> zaszyfrowaneLiczby = new List<int>();

                // Iteruj przez znaki w tekście
                foreach (char znak in tekst)
                {
                    // Jeśli znak jest literą, to znajdź jego pozycję w macierzy
                    if (char.IsLetter(znak))
                    {
                        int rowIndex = -1;
                        int colIndex = -1;

                        // Przeszukaj macierz w poszukiwaniu znaku
                        for (int i = 0; i < model.Macierz.Length; i++)
                        {
                            for (int j = 0; j < model.Macierz[i].Length; j++)
                            {
                                if (model.Macierz[i][j].ToLower() == znak.ToString())
                                {
                                    rowIndex = i;
                                    colIndex = j;
                                    break;
                                }
                            }
                            if (rowIndex >= 0 && colIndex >= 0)
                                break;
                        }

                        // Jeśli znaleziono znak w macierzy, to dodaj jego pozycję jako int do listy
                        if (rowIndex >= 0 && colIndex >= 0)
                        {
                            int pozycja = (rowIndex + 1) * 10 + (colIndex + 1); // Tworzymy jedną liczbę z dwóch pozycji
                            zaszyfrowaneLiczby.Add(pozycja);
                        }
                    }
                }

                // Podnieś każdą liczbę w liście do 5 potęgi
                List<int> zaszyfrowanePotegi = new List<int>();
                foreach (int liczba in zaszyfrowaneLiczby)
                {
                    int potega = (int)Math.Pow(liczba, 5);
                    zaszyfrowanePotegi.Add(potega);
                }

                model.ZaszyfrowanyTekst = string.Join(" ", zaszyfrowanePotegi);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Deszyfrowanie()
        {
            return View(new Polibusz());
        }
        [HttpPost]
        public IActionResult Deszyfrowanie(Polibusz model)
        {
            if (ModelState.IsValid)
            {
                string zaszyfrowanyTekst = model.Tekst;

                // Rozdziel zaszyfrowany tekst na pary liczb
                string[] paryLiczb = zaszyfrowanyTekst.Split(' ');

                // Inicjalizuj wynik jako pusty ciąg znaków
                string odszyfrowanyTekst = "";

                // Iteruj przez pary liczb
                foreach (string para in paryLiczb)
                {
                    if (int.TryParse(para, out int liczba))
                    {
                        // Podnieś liczbę do piątej potęgi
                        int potega = (int)Math.Pow(liczba, 1.0 / 5);

                        // Odczytaj indeksy wiersza i kolumny z pary liczb
                        int rowIndex = (potega / 10) - 1;
                        int colIndex = (potega % 10) - 1;

                        // Sprawdź, czy indeksy są w prawidłowym zakresie
                        if (rowIndex >= 0 && rowIndex < model.Macierz.Length && colIndex >= 0 && colIndex < model.Macierz[0].Length)
                        {
                            // Pobierz znak z macierzy na podstawie indeksów
                            string znak = model.Macierz[rowIndex][colIndex];
                            odszyfrowanyTekst += znak;
                        }
                    }
                }

                model.ZaszyfrowanyTekst = odszyfrowanyTekst;
            }

            return View(model);
        }


    }
}