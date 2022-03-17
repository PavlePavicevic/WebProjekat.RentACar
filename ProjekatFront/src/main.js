import { Iznajmljivanje } from "./Iznajmljivanje.js";
import { Ekspozitura } from "./Ekspozitura.js";
import { IzmenaAutomobila } from "./IzmenaAutomobila.js";
import { DodavanjeKorisnika } from "./DodavanjeKorisnika.js";


fetch("https://localhost:5001/Ekspozitura/PrikaziEkspoziture", {
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(ekspoziture => {
                    ekspoziture.forEach(ekspozitura => {
                        let exp = new Ekspozitura(ekspozitura.ime, ekspozitura.grad);
                        exp.crtaj(document.body);
                    })
                    let auto = new IzmenaAutomobila();
                    auto.crtaj(document.body);
                    let korisnici = new DodavanjeKorisnika();
                    korisnici.crtaj(document.body);
                })
            }
        })