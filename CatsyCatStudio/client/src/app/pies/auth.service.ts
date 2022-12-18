import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { PieService } from "./pie.service";

@Injectable({
    providedIn:"root"
})
export class AuthService implements CanActivate{

    constructor(private route:Router,public pieService:PieService) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

        if (this.pieService.loginRequired) {
            this.route.navigate(["/login"]);
            return false;
          } else {
            return true;
          }
    }


}