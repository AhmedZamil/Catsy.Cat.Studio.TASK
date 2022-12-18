import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { PieService } from "../pies/pie.service";
import { LoginRequest } from "./loginRequest";

@Component({
    templateUrl:'./login.component.html',
    styleUrls:['./login.component.css']
})
export class LoginComponent implements OnInit{

    constructor(private Route:ActivatedRoute,private router:Router,public pieService: PieService) {
        
    }

    // public token ="";
    // public expiration= new Date();

    // public creds = {
    //     "username":"rihan@gmail.com",
    //     "password":"Ahmed@123"
    // };

    public creds: LoginRequest = {
        username: "",
        password: ""
      };
    auth! : Subscription; 
    public errorMessage = "";
    ngOnInit(): void {
        
    }

    onLogin():void{
        this.auth = this.pieService.getAuth(this.creds).subscribe({
            next: ()=> {
              this.router.navigate(['/pies']);
                // this.pieService.token = data.token;
                // this.pieService.expiration = data.expiration;
               }
        });
    
    }

}