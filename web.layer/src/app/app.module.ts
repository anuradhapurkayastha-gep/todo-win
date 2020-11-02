import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Header
import {HeaderComponent} from './header/header.component';
// left Pannel 
import {LeftPannelComponent} from './left-pannel/pannel.component';
// Table
import {TableComponent} from './table/table.component';

import {HttpService} from './http.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LeftPannelComponent,
    TableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [HttpService],
  entryComponents:[],
  bootstrap: [AppComponent]
})
export class AppModule { }
