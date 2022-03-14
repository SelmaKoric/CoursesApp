import { Injectable } from '@angular/core';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ObavijestiApiService {

  readonly apiUrl="https://p2058.app.fit.ba/api";

  constructor(private http:HttpClient) { }

  getObavijestiList():Observable<any[]>{
    return this.http.get<any>(this.apiUrl+'/Obavijest/GetObavijesti');
  }

  dodajObavijest(data:any){
    return this.http.post(this.apiUrl+"/Obavijest/PostObavijest",data,);
  }

  urediObavijest(Id:number|string, data: any){
    return this.http.put(this.apiUrl+ '/Obavijest/PutObavijest?Id='+Id,data);
  }

  obrišiObavijest(id:number|string){
    return this.http.delete(this.apiUrl+'/Obavijest/DeleteObavijest',{params: {'id':id}});
  }

  //TipObavijesti
  getTipObavijestiList():Observable<any[]>{
    return this.http.get<any>(this.apiUrl+'/TipObavijesti/GetTipObavijesti');
  }

  dodajTipObavijest(data:any){
    return this.http.post(this.apiUrl+'/TipObavijesti/PostTipObavijest', data);
  }

  urediTipObavijest(Id:number|string, data: any){
    return this.http.put(this.apiUrl+ '/TipObavijesti/PutTipObavijest/'+Id, data);
  }

  obrišiTipObavijest(Id:number|string){
    return this.http.delete(this.apiUrl+'TipObavijesti/DeleteTipObavijest/'+Id);
  }
}
