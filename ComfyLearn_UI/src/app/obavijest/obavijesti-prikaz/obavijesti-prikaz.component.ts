import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {ObavijestiApiService} from 'src/app/obavijesti-api.service'

@Component({
  selector: 'app-obavijesti-prikaz',
  templateUrl: './obavijesti-prikaz.component.html',
  styleUrls: ['./obavijesti-prikaz.component.css']
})
export class ObavijestiPrikazComponent implements OnInit {

 listaObavijesti$!:Observable<any[]>;
 listaTipObavijesti$!:Observable<any[]>;
 listaTipObavijesti:any=[];

 tipObavijestiMap:Map<number,string>=new Map<number, string>();

  constructor(private service: ObavijestiApiService) { }

  ngOnInit(): void {
    this.listaObavijesti$=this.service.getObavijestiList();
    this.listaTipObavijesti$=this.service.getTipObavijestiList();
    this.refreshTipObavijestiMap();
  }

  //properties
  modalTitle:string='';
  activateDodajUrediObavijestComponent:boolean=false;
  obavijest:any;

  modalAdd(){
    this.obavijest={
      id:0,
      tipObavijestiId:null,
      naslov:null,
      sadrzaj:null,
      datumObavijesti:null
    };
    this.modalTitle="Dodaj novu obavijest";
    this.activateDodajUrediObavijestComponent=true;
  }

  modalEdit(item:any){
    this.obavijest=item;
    this.modalTitle="Uredi obavijest";
    this.activateDodajUrediObavijestComponent=true;
  }

  ukloni(item:any){
    if(confirm("Da li želite obrisati objavu "+item.id + '?')){
this.service.obrišiObavijest(item.id).subscribe(result=>{
  var closeModalBtn=document.getElementById("delete-modal-close");
  if(closeModalBtn){
    closeModalBtn.click();
  }

  var showDeleteSuccess=document.getElementById("delete-success-alert");
  if(showDeleteSuccess){
    showDeleteSuccess.style.display="block";
  }
  setTimeout(function (){
    if(showDeleteSuccess){
      showDeleteSuccess.style.display="none";
    }
  }, 4000);
  this.listaObavijesti$=this.service.getObavijestiList();
})
    }
  }

  modalClose(){
  this.activateDodajUrediObavijestComponent=false;
  this.listaObavijesti$=this.service.getObavijestiList();
  }

  refreshTipObavijestiMap(){
    this.service.getTipObavijestiList().subscribe(data=>{
      this.listaTipObavijesti=data;

      for (let i =0; i<data.length; i++){
        this.tipObavijestiMap.set(this.listaTipObavijesti[i].id,this.listaTipObavijesti[i].naziv);
      }
    })
  }
}
