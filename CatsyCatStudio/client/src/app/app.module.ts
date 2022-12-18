import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import { AppComponent } from './app.component';
import { PieListComponent } from './pies/pie-list.component';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './shared/login.component';
import { AuthService } from './pies/auth.service';
import { FormsModule } from '@angular/forms';
import { ShopComponent } from './shop/shop.component';
import { PieDetailComponent } from './pies/pie-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    PieListComponent,
    LoginComponent,
    ShopComponent,
    PieDetailComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path:"pies",component:PieListComponent,canActivate:[AuthService]},
      {path:"pies/:pieId",component:PieDetailComponent},
      {path:"login",component:LoginComponent},
      {path:"ngshop",component:ShopComponent},
      {path:" ",redirectTo:"ngshop",pathMatch:"full"},
      {path:"**",redirectTo:"ngshop",pathMatch:"full"}
    ],{
      useHash:true
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
