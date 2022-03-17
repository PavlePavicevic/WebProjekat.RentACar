export class Automobili{
    constructor() {
        this.kontejner = null;
    }

    crtaj(kontejner,imeGrada) {
        this.kontejner = kontejner;

        fetch("https://localhost:5001/Auto/PrikaziAutomobile/"+imeGrada,{
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
        divZaTabelu.classList.add("tabelaOkvirAuto");
        this.kontejner.appendChild(divZaTabelu);

        let tabela = document.createElement("table");
        this.tabela = tabela;
        tabela.className = "tabelaAuto";
        divZaTabelu.appendChild(tabela);

        let tabHeader = document.createElement("tr");
        tabela.appendChild(tabHeader);

        let th;
        const kolone = ["Registracija", "Marka", "Godiste", "Kilometraza", "Lokacija", "Cena","Izmena kilometraze"];
        kolone.forEach(podatak => {
            th = document.createElement("th");
            th.innerHTML = podatak;
            tabHeader.appendChild(th);
        })

        let td, btn;
        podaci.forEach(podatak => {
            this.dodajRed(podatak);
        })
    }

    dodajRed(podatak) {
        let tr,td,btn,inp;
        tr = document.createElement("tr");
        this.tabela.appendChild(tr);        

        td = document.createElement("td");
        td.innerHTML = podatak.registracioniBroj;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.marka;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.godinaProizvodnje;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.kilometraza;
        let kilo = td;
        tr.appendChild(td);            

        td = document.createElement("td");
        td.innerHTML = podatak.lokacija;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = podatak.cena;
        tr.appendChild(td);

        td = document.createElement("td");
        btn = document.createElement("button");
        inp = document.createElement("input");
        inp.type = "number";
        inp.placeholder = "Nova vrednost...";
        inp.className = "kilometraza";
        btn.className = "btnBrisanje";
        btn.innerHTML = "Promeni";
        btn.onclick = () => {
            fetch("https://localhost:5001/Auto/PromeniPodatkeOAutu/" + podatak.registracioniBroj+ "/" + inp.value, {
                method: "PUT"
            }).then((s) => {
                if (s.ok) {
                    alert("Kilometraza izmenjena");
                    kilo.innerHTML = inp.value;
                    inp.value = "";
                }
                else {
                    alert("Doslo je do greske prilikom izmene kilometraze!")
                }
            });
        }
        td.appendChild(inp)
        td.appendChild(btn);
        tr.appendChild(td);
    }
}