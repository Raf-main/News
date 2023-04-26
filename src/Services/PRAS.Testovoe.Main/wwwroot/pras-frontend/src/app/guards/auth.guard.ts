import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(private authService: AuthService){

  }

  canActivate (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if(this.authService.isAuthenticated()){
      return true;
    }
    return false
  }
}
