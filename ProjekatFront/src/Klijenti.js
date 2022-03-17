export class Klijenti{
    constructor() {
        this.kontejner = null;
    }

    crtaj(kontejner) {
        this.kontejner=kontejner;

        fetch("https://localhost:5001/Klijent/PrikaziKlijente/",{
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(podaci => {
                    this.crtajTabelu(podaci);
                })
            }
        })
    }

    crtajTabelu(podaci) {
        let divZaTabelu = document.createElement("div");
        divZaTabelu.className = "tabelaOkvir";
        this.kontejner.appendChild(divZaTabelu);

        let tabela = document.createElement("table");
        this.tabela = tabela;
        tabela.className = "tabelaKlijent";
        divZaTabelu.appendChild(tabela);

        let tabHeader = document.createElement("tr");
        tabela.appendChild(tabHeader);

        let th;
        const kolone = ["Ime", "Prezime", "Broj licne karte"];
        kolone.forEach(podatak => {
            th = document.createElement("th");
            th.innerHTML = podatak;
            tabHeader.appendChild(th);
        })

        
        podaci.forEach(podatak => {
            this.dodajRed(podatak);
        })
    }

    dodajRed(podatak) {

        let tr,td,btn;
        tr = document.createElement("tr");
        this.tabela.appendChild(tr);        

        td = document.createElement("td");
        td.innerHTML = podatak.ime;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.prezime;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.brojLicneKarte;
        tr.appendChild(td);

        td = document.createElement("td");
        btn = document.createElement("button");
        btn.className = "btnBrisanje";
        btn.innerHTML = "Obrisi";
        btn.onclick = () => {
            fetch("https://localhost:5001/Klijent/UkloniKlijenta/" + podatak.brojLicneKarte, {
                method: "DELETE"
            }).then((s) => {
                if (s.ok) {
                    alert("Klijent je obrisan!");
                }
                else {
                    alert("Doslo je do greske prilikom brisanja klijenta!")
                }
            });
            tr.remove();
        }

        td.appendChild(btn)
        tr.appendChild(td);
        
    }

}
