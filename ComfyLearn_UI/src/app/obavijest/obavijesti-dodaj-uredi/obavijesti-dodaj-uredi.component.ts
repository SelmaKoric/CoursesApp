import { Component, Input, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {ObavijestiApiService} from "../../obavijesti-api.service";

@Component({
  selector: 'app-obavijesti-dodaj-uredi',
  templateUrl: './obavijesti-dodaj-uredi.component.html',
  styleUrls: ['./obavijesti-dodaj-uredi.component.css']
})
export class ObavijestiDodajUrediComponent implements OnInit {

  listaObavijesti$!:Observable<any[]>;
  listaTipoviObavijesti$!:Observable<any[]>;


  constructor(private service:ObavijestiApiService) { }

  @Input() obavijest:any;
  id:number=0;
  naslov:string="";
  sadrzaj:string="";
  tipObavijestiId!:number;
  datumObavijesti:any=new Date().toISOString();

  ngOnInit(): void {
    this.id=this.obavijest.id;
    this.naslov=this.obavijest.naslov;
    this.sadrzaj=this.obavijest.sadrzaj;
    this.datumObavijesti=this.obavijest.datumObavijesti;
    this.tipObavijestiId=this.obavijest.tipObavijestiId;
    this.listaObavijesti$=this.service.getObavijestiList();
    this.listaTipoviObavijesti$=this.service.getTipObavijestiList();
  }

  DodajObavijest(){
    var obavijest={
      naslov:this.naslov,
      tipObavijestiId:this.tipObavijestiId,
      sadrzaj:this.sadrzaj,
      datumObavijesti: this.datumObavijesti
    }
this.service.dodajObavijest(obavijest).subscribe(result=>{
  var closeModalBtn=document.getElementById("add-edit-modal-close");
  if(closeModalBtn){
    closeModalBtn.click();
  }

  var showAddSuccess=document.getElementById("add-success-alert");
  if(showAddSuccess){
    showAddSuccess.style.display="block";
  }
  setTimeout(function (){
    if(showAddSuccess){
      showAddSuccess.style.display="none";
    }
  }, 4000);
})
  }

  UrediObavijest(){
    let obavijest={
      id:this.id,
      naslov:this.naslov,
      tipObavijestiId:this.tipObavijestiId,
      sadrzaj:this.sadrzaj,
      datumObavijesti: this.datumObavijesti
    }
    var id:number=this.id;

    this.service.urediObavijest(id,obavijest).subscribe(result=>{
      var closeModalBtn=document.getElementById("add-edit-modal-close");
      if(closeModalBtn){
        closeModalBtn.click();
      }

      var showUpdateSuccess=document.getElementById("update-success-alert");
      if(showUpdateSuccess){
        showUpdateSuccess.style.display="block";
      }
      setTimeout(function (){
        if(showUpdateSuccess){
          showUpdateSuccess.style.display="none";
        }
      }, 4000);
    })
  }
}
