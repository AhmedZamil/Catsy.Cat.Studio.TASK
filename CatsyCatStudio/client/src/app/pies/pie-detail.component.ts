import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IPie } from './pie';
import { PieService } from './pie.service';

@Component({
  templateUrl:'./pie-detail.component.html' ,
  styleUrls: ['./pie-detail.component.css']
})
export class PieDetailComponent implements OnInit {

  constructor(private route:ActivatedRoute,private router:Router,private pieService:PieService) { }

  pie:IPie | undefined ;
  sub!:Subscription;
  errorMessage:string = "";

  ngOnInit(): void {
  let pieId = Number(this.route.snapshot.paramMap.get("pieId"));
  this.sub = this.pieService.getPieById(pieId).subscribe({
    next: pie => {this.pie = pie;
                  alert(this.pie.pieId)},
    error:err=> this.errorMessage = err.error.message
  });
  }

}
