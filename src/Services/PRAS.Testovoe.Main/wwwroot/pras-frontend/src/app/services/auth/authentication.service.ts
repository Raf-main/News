import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { LocalStorageService } from "../storages/local-storage.service";
import { LoginResponse } from "../../models/response/loginResponse";
import { RefreshTokenResponse } from 'src/app/models/response/refreshTokenResponse';
import { UserResponse } from 'src/app/models/response/userResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl: string = environment.apiUrl + '/Account/Login';
  private registerUrl: string = environment.apiUrl + '/Account/Register';
  private refreshTokenUrl: string = environment.apiUrl + '/Account/RefreshToken';
  private emailExistsUrl: string = environment.apiUrl + '/Account/EmailExists';
  private readonly tokenKey: string = 'token';
  private readonly userKey: string = 'user';

  constructor(private http: HttpClient, private localStorageService: LocalStorageService, private router: Router) {

  }

  login(data: any): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.loginUrl, data);
  }

  register(data: any): Observable<any> {
    return this.http.post(this.registerUrl, data);
  }

  refreshToken() : Observable<RefreshTokenResponse> {
    return this.http.post<RefreshTokenResponse>(this.refreshTokenUrl, "");
  }

  emailExists(email: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.emailExistsUrl}?email=${email}`);
  }

  saveToken(token: string): void {
    this.localStorageService.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    let token: string | null = this.localStorageService.getItem(this.tokenKey);

    if (token) {
      return token
    }

    return null
  }

  isAuthenticated(): boolean {
    if (this.getToken() != null) {
      return true;
    }

    return false;
  }

  saveUser(userResponse: UserResponse) {
    console.log(userResponse);
    let user = JSON.stringify(userResponse);
    this.localStorageService.setItem(this.userKey, user);
  }

  isInRole(role: string) : boolean {
    let jsonUser = this.localStorageService.getItem(this.userKey);

    if(jsonUser == null){
      return false;
    }

    let user: UserResponse = JSON.parse(jsonUser);

    return user.roles.includes(role);
  }

  logout(): void {
    this.localStorageService.removeItem(this.tokenKey);
    this.localStorageService.removeItem(this.userKey);
    this.router.navigate(['/']);
  }
}
