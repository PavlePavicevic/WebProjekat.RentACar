export class Iznajmljivanje{
    constructor(imeEkspoziture) {
        this.kontejner = null;
        this.podaci = null;
        this.imeEkspoziture = imeEkspoziture;
        this.tabela = null;
    }

    crtaj(kontejner) {
        this.kontejner = kontejner;
        fetch("https://localhost:5001/Iznajmljivanja/PrikaziIznajmljivanja/"+this.imeEkspoziture, {
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(iznajmljivanja => {
                    this.podaci = iznajmljivanja;
                    this.crtajTabelu();
                })
            }
        })
    }

    crtajTabelu() {
        let divZaTabelu = document.createElement("div");
        divZaTabelu.className = "tabelaOkvir";
        this.kontejner.appendChild(divZaTabelu);

        let tabela = document.createElement("table");
        this.tabela = tabela;
        tabela.className = "tabela";
        divZaTabelu.appendChild(tabela);

        let tabHeader = document.createElement("tr");
        tabela.appendChild(tabHeader);

        let th;
        const podaci = [ "Marka auta", "Registarski broj", "Ime", "Prezime", "Datum", "Dana", "Obrisi"];
        podaci.forEach(podatak => {
            th = document.createElement("th");
            th.innerHTML = podatak;
            tabHeader.appendChild(th);
        })

        
        this.podaci.forEach(podatak => {
            this.dodajRed(podatak);
        })
    }

    dodajRed(podatak) {
        let tr,td,btn;
        tr = document.createElement("tr");
        this.tabela.appendChild(tr);

        

        td = document.createElement("td");
        td.innerHTML = podatak.markaAuta;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.registarskiBroj;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.klijentIme;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.klijentPrezime;
        tr.appendChild(td);            

        td = document.createElement("td");
        td.innerHTML = podatak.datumIznajmljivanja;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.brojDana;
        tr.appendChild(td);

        td = document.createElement("td");
        btn = document.createElement("button");
        btn.className = "btnBrisanje";
        btn.innerHTML = "Obrisi";
        btn.onclick = () => {
            fetch("https://localhost:5001/Iznajmljivanja/IzbrisiIznajmljivanje/" + podatak.ekspozitura + "/" + podatak.klijentIme + "/" + podatak.klijentPrezime + "/" + podatak.registarskiBroj, {
                method: "DELETE"
            }).then((s) => {
                if (s.ok) {
                    alert("Iznajmljivanje je izbrisano");
                }
                else {
                    alert("Doslo je do greske prilikom brisanja iznajmljivanja!")
                }
            });
            tr.remove();
        }
        td.appendChild(btn)
        tr.appendChild(td);
    }

   
}