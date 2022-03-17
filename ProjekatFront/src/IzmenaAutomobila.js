import { Automobili } from "./Automobili.js"

export class IzmenaAutomobila{

    constructor(){
        this.kontejner = null;
        this.tabela = null;
    }

    crtaj(kontejner) {
        this.kontejner = kontejner;
        let divZaAuto = document.createElement("div");
        divZaAuto.className = "autoDiv";
        kontejner.appendChild(divZaAuto);
        
        let divZaKontrole = document.createElement("div");
        divZaKontrole.className = "kontroleDiv";
        divZaKontrole.classList.add("kontroleAuto");
        divZaAuto.appendChild(divZaKontrole);

        let naslov = document.createElement("h3");
        naslov.innerHTML = "Dodavanje automobila";
        divZaKontrole.appendChild(naslov)

        let lbl = document.createElement("label");
        lbl.innerHTML = "Registarski broj:";
        divZaKontrole.appendChild(lbl);
        let regNum = document.createElement("input");
        regNum.type = "text";
        divZaKontrole.appendChild(regNum);

        lbl = document.createElement("label");
        lbl.innerHTML = "Marka auta:";
        divZaKontrole.appendChild(lbl);
        let markaAuta = document.createElement("input");
        markaAuta.type = "text";
        divZaKontrole.appendChild(markaAuta);

        lbl = document.createElement("label");
        lbl.innerHTML = "Godina proizvodnje:";
        divZaKontrole.appendChild(lbl);
        let godNum = document.createElement("input");
        godNum.type = "number";
        divZaKontrole.appendChild(godNum);

        lbl = document.createElement("label");
        lbl.innerHTML = "Kilometraza:";
        divZaKontrole.appendChild(lbl);
        let kilometraza = document.createElement("input");
        kilometraza.type = "number";
        divZaKontrole.appendChild(kilometraza);

        lbl = document.createElement("label");
        lbl.innerHTML = "Grad:";
        divZaKontrole.appendChild(lbl);
        let gradSelect = document.createElement("select");
        gradSelect.class = "gradSelect";
        divZaKontrole.appendChild(gradSelect);
        this.ucitajGradove(gradSelect);
        
        lbl = document.createElement("label");
        lbl.innerHTML = "Cena iznajmljivanja:";
        divZaKontrole.appendChild(lbl);
        let cenaIznajmljivanja = document.createElement("input");
        cenaIznajmljivanja.type = "number";
        divZaKontrole.appendChild(cenaIznajmljivanja);
        
        let btnDodaj = document.createElement("button");
        btnDodaj.innerHTML = "Dodaj auto";
        btnDodaj.onclick = () => {
            fetch("https://localhost:5001/Auto/DodajAuto/" + regNum.value + "/" + markaAuta.value + "/" + godNum.value + "/" + kilometraza.value + "/" + cenaIznajmljivanja.value + "/" + gradSelect.value, {
                method:"POST"
            }).then((s) => {
                if (s.ok) {
                    document.querySelector(".tabelaOkvirAuto").remove();
                    this.tabela= new Automobili();
                    this.tabela.crtaj(divZaAuto, gradSelect.value);

                    regNum.value= null;
                    markaAuta.value= null;
                    godNum.value= null;
                    kilometraza.value= null;
                    cenaIznajmljivanja.value= null;
                    gradSelect.value= null;
                }
            })
        }
        divZaKontrole.appendChild(btnDodaj);

        let btnPrikazi = document.createElement("button");
        btnPrikazi.innerHTML = "Prikazi automobile";
        btnPrikazi.onclick = () => {
            document.querySelector(".tabelaOkvirAuto").remove();
            this.tabela= new Automobili();
            this.tabela.crtaj(divZaAuto, gradSelect.value);
        }
        divZaKontrole.appendChild(btnPrikazi);
        
        let tabelaAuto = new Automobili();
        tabelaAuto.crtaj(divZaAuto, "Nis");
        this.tabela = tabelaAuto;
    }

    ucitajGradove(autoSelect) {
        fetch("https://localhost:5001/Ekspozitura/PrikaziEkspoziture", {
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(ekspoziture => {
                    let gradOpt;
                    ekspoziture.forEach(ekspozitura => {
                        gradOpt = document.createElement("option");
                        gradOpt.innerHTML = ekspozitura.grad;
                        gradOpt.value = ekspozitura.grad;
                        autoSelect.appendChild(gradOpt);
                    })
                })
            }
        })
    }
}