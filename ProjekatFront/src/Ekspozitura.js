import { Iznajmljivanje } from "./Iznajmljivanje.js";

export class Ekspozitura{
    constructor(imeEkspoziture,grad) {
        this.imeEkspoziture = imeEkspoziture;
        this.grad = grad;
        this.kontejner = null;
    }

    crtaj(kontejner) {
        this.kontejner = kontejner;        

        let divZaExp = document.createElement("div");
        divZaExp.className = "expozituraDiv";
        this.kontejner.appendChild(divZaExp);  

        let divZaKontrole = document.createElement("div");
        divZaKontrole.className = "kontroleDiv";
        divZaExp.appendChild(divZaKontrole);

        let naslov = document.createElement("h3");
        naslov.innerHTML = this.imeEkspoziture;
        divZaKontrole.appendChild(naslov)

        let lbl = document.createElement("label");
        lbl.innerHTML = "Ime:";
        divZaKontrole.appendChild(lbl);
        let imeTxt = document.createElement("input");
        imeTxt.type = "text";
        imeTxt.className = "imeInput";
        divZaKontrole.appendChild(imeTxt);

        lbl = document.createElement("label");
        lbl.innerHTML = "Prezime:";
        divZaKontrole.appendChild(lbl);
        let prezimeTxt = document.createElement("input");
        prezimeTxt.type = "text";
        prezimeTxt.className = "prezimeInput";
        divZaKontrole.appendChild(prezimeTxt);

        lbl = document.createElement("label");
        lbl.innerHTML = "Automobil:";
        divZaKontrole.appendChild(lbl);
        let autoSelect = document.createElement("select");
        autoSelect.className = "autoSelect";
        divZaKontrole.appendChild(autoSelect);
        this.ucitajAutomobile(autoSelect);

        lbl = document.createElement("label");
        lbl.innerHTML = "Broj dana:";
        divZaKontrole.appendChild(lbl);
        let daniNum = document.createElement("input");
        daniNum.type = "number";
        daniNum.className = "daniInput";
        divZaKontrole.appendChild(daniNum);

        let dugmeIznajmi = document.createElement("button");
        dugmeIznajmi.innerHTML = "Iznajmi";
        dugmeIznajmi.onclick = () => {
            fetch("https://localhost:5001/Iznajmljivanja/DodajIznajmljivanje/" + this.imeEkspoziture + "/" + autoSelect.value + "/" + imeTxt.value + "/" + prezimeTxt.value + "/" + daniNum.value, {
                method: "POST"
            }).then((s) => {
                if (s.ok) {
                    s.json().then(auto => {
                        let datum = new Date(auto.datumIznajmljivanja);
                        const mesec = datum.toLocaleString('en-US', {month: 'short'});
                        this.tabela.dodajRed({
                            ekspozitura: auto.ekspozitura.ime,
                            markaAuta: auto.auto.markaAuta,
                            registarskiBroj: autoSelect.value,
                            klijentIme: imeTxt.value,
                            klijentPrezime: prezimeTxt.value,
                            datumIznajmljivanja: datum.getDate()+"-"+mesec+"-"+(parseInt(datum.getFullYear())-2000),
                            brojDana:auto.brojDana
                        })
                        imeTxt.value= null;
                        prezimeTxt.value= null;
                        daniNum.value= null;
                    })

                }
            });
        }

        this.tabela = new Iznajmljivanje(this.imeEkspoziture);
        this.tabela.crtaj(divZaExp);
        divZaKontrole.appendChild(dugmeIznajmi);
        
    }

    ucitajAutomobile(autoSelect) {
        fetch("https://localhost:5001/Auto/PrikaziAutomobile/" + this.grad, {
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(autos => {
                    let autoOpt;
                    autos.forEach(auto => {
                        autoOpt = document.createElement("option");
                        autoOpt.innerHTML = auto.marka;
                        autoOpt.value = auto.registracioniBroj;
                        autoSelect.appendChild(autoOpt);
                    })
                })
            }
        })
    }
}