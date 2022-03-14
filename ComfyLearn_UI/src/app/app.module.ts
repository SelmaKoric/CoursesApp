import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ObavijestComponent } from './obavijest/obavijest.component';
import { ObavijestiPrikazComponent } from './Obavijest/obavijesti-prikaz/obavijesti-prikaz.component';
import { ObavijestiDodajUrediComponent } from './Obavijest/obavijesti-dodaj-uredi/obavijesti-dodaj-uredi.component';
import {ObavijestiApiService} from "./obavijesti-api.service";

@NgModule({
  declarations: [
    AppComponent,
    ObavijestComponent,
    ObavijestiPrikazComponent,
    ObavijestiDodajUrediComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [ObavijestiApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
