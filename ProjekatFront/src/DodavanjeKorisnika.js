import { Klijenti } from "./Klijenti.js"

export class DodavanjeKorisnika{
    constructor() {
        this.kontejner = null;
    }
    crtaj(kontejner){
        this.kontejner = kontejner;

        
        let divZaKlijenta = document.createElement("div");
        divZaKlijenta.className = "klijentDiv";
        kontejner.appendChild(divZaKlijenta);
        
        let divZaKontrole = document.createElement("div");
        divZaKontrole.className = "kontroleDiv";
        divZaKontrole.classList.add("kontroleKlijent");
        divZaKlijenta.appendChild(divZaKontrole);

        let naslov = document.createElement("h3");
        naslov.innerHTML = "Dodavanje klijenta";
        divZaKontrole.appendChild(naslov)

        let lbl = document.createElement("label");
        lbl.innerHTML = "Ime:";
        divZaKontrole.appendChild(lbl);
        let ime = document.createElement("input");
        ime.type = "text";
        divZaKontrole.appendChild(ime);

        lbl = document.createElement("label");
        lbl.innerHTML = "Prezime:";
        divZaKontrole.appendChild(lbl);
        let prezime = document.createElement("input");
        prezime.type = "text";
        divZaKontrole.appendChild(prezime);

        lbl = document.createElement("label");
        lbl.innerHTML = "Broj licne karte:";
        divZaKontrole.appendChild(lbl);
        let brLicneKarte = document.createElement("input");
        brLicneKarte.type = "text";
        divZaKontrole.appendChild(brLicneKarte);

        
        let btnDodaj = document.createElement("button");
        btnDodaj.innerHTML = "Dodaj klijenta";
        btnDodaj.onclick = () => {
            fetch("https://localhost:5001/Klijent/DodajKlijenta/" + ime.value + "/" + prezime.value + "/" + brLicneKarte.value , {
                method:"POST"
            }).then((s) => {
                if (s.ok) {
                    document.querySelector(".tabelaKlijent").remove();
                    this.tabela= new Klijenti();
                    this.tabela.crtaj(divZaKlijenta);

                    ime.value = null;
                    prezime.value = null;
                    brLicneKarte.value = null;
                }
            })
        }
        
        
        divZaKontrole.appendChild(btnDodaj);
        
        let tabelaKlijent = new Klijenti();
        tabelaKlijent.crtaj(divZaKlijenta);
        this.tabela = tabelaKlijent;
    }
}
