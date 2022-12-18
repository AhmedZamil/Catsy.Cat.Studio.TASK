import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { IPie } from "../pies/pie";
import { PieService } from "../pies/pie.service";

@Component({
    templateUrl:'./shop.component.html',
    styleUrls:['./shop.component.css']
})
export class ShopComponent implements OnInit,OnDestroy{

    constructor(private pieService:PieService) 
        { }

    pageTitle:string="This is a Open Shop";
    sub! :Subscription;
    allPies:IPie[] | undefined;
    errorMessage:string="";


    ngOnInit(): void {
        this.sub = this.pieService.getAllPies().subscribe({
            next:pies =>{
                this.allPies = pies;
            },
            error:err=> this.errorMessage = err.error.message
        }
        );
    }
    ngOnDestroy(): void {

    }

}