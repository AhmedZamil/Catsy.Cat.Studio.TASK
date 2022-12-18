import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IPie } from './pie';
import { PieService } from './pie.service';

@Component({
    selector: 'app-pie-list',
    templateUrl: "./pie-list.component.html",
    styleUrls: ["./pie-list.component.css"]
})
export class PieListComponent implements OnInit {

  pageTitle:string ="";
  showImage:boolean= true;
  imageWidth:number = 50;
  imageMargin:number=4;

  pies!: IPie[];
  sub!: Subscription;

  public errorMessage="";


  constructor(private pieService:PieService) { }

  ngOnInit(): void {

     this.sub = this.pieService.getPies().subscribe({
         next: pies=> {
             this.pies = pies;
            },
         error: err => this.errorMessage = err
     });
  }

}
