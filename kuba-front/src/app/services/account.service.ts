import { Injectable } from '@angular/core';
import {  Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../environment';
import {  UserResponseLogin } from '../shared/models/user-response-login';
import { UserRequestLogin } from '../shared/models/user-request-login';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private router: Router) {
    // this.loadCurrentUserFromLocalStorage(); 
  }

 public login(userRequestLogin: UserRequestLogin): Observable<UserResponseLogin> {
  return this.http.post<UserResponseLogin>(`${this.baseUrl}/account/login`, userRequestLogin);
  }

  getUserRole() {
    return localStorage.getItem('role');

  }

  // logout() {
  //   this.removeLocalStorageItem('token');
  //   this.removeLocalStorageItem('id');
  //   this.currentUserSource.next(null);
  //   this.router.navigateByUrl('/');
  // }
}
