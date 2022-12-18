import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { Observable, Subscription, throwError } from "rxjs";
import {tap,catchError, map} from "rxjs/operators";
import { LoginRequest } from "../shared/loginRequest";
import { LoginResults } from "../shared/loginResults";
import { IPie } from "./pie";


@Injectable({
    providedIn:'root'
})
export class PieService implements OnInit{
    /**
     *
     */
    constructor(private http:HttpClient) {

        // this.http.post<LoginResults>("http://localhost:8888/account/createtoken", this.creds).pipe(map(data => {
        //     this.token = data.token;
        //     this.expiration = data.expiration;
        //   }));

        // this.auth = this.getAuth().subscribe({
        //     next: data=> {
        //         this.token = data.token;
        //         this.expiration = data.expiration;
        //         this.httpOptions={
        //             headers: new HttpHeaders({
        //                 'Content-Type': 'application/json',
        //                 'Authorization': 'Bearer '+ this.token
        //               })
        //         }
        //        }
        // });
    }
    ngOnInit(): void {
   
    }

    public token ="";
    public expiration= new Date();

    // public creds = {
    //     "username":"rihan@gmail.com",
    //     "password":"Ahmed@123"
    // };
    auth! : Subscription; 

    private serviceUrl:string = "http://localhost:8888/api/PieAPi";
    private getAllPiesUrl:string="http://localhost:8888/api/PieAPi/All";
    private baseUrl:string="http://localhost:8888/api/PieAPi/";

    public httpOptions = {

      };

    public get loginRequired():boolean{
        return this.token.length==0 || this.expiration > new Date();
    }

    // login(creds: LoginRequest) {
    //     return this.http.post<LoginResults>("/account/createtoken", creds)
    //       .pipe(map(data => {
    //         this.token = data.token;
    //         this.expiration = data.expiration;
    //       }));
    //   }
    

    getAuth(creds: LoginRequest):Observable<void>{
        return this.http.post<LoginResults>("http://localhost:8888/account/createtoken", creds)
        .pipe(map(data=>{
            this.token = data.token;
            this.expiration = data.expiration;
        }));
    }

    getPies():Observable<IPie[]> {
         this.httpOptions={
           headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer '+ this.token
              })
        }

        return this.http.get<IPie[]>(this.serviceUrl,this.httpOptions).pipe(
            tap(data=>console.log("Data: ",JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getAllPies(): Observable<IPie[]>{

        return this.http.get<IPie[]>(this.getAllPiesUrl).pipe(
            tap(pies=>console.log("All Pies:",JSON.stringify(pies))),
            catchError(this.handleError)
        );
    }

    getPieById(pieId:number): Observable<IPie>{
        var getPieByIdUrl = this.baseUrl + pieId.toLocaleString();
        return this.http.get<IPie>(getPieByIdUrl).pipe(
            tap((pie=>console.log("Pie : ",JSON.stringify(pie)))),
            catchError(this.handleError)
        );

    }

    private handleError(err:HttpErrorResponse){

        let errorMessage:string ="";

        if(err.error instanceof ErrorEvent)
        {
            errorMessage = `error Message ${err.error.message}`;
        }
        else{
            errorMessage = `error Message ${err.message}`;
        }
        return throwError(errorMessage);
    }

}