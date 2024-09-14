import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../interface/user";
import {URL_LOGIN, URL_SIGN, URL_USER} from "./url";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  private setHttpHeader() {
    const token = localStorage.getItem('token');

    let bearerToken;
    let header;

    if (token) {
      bearerToken = `Bearer ${token}`

      header = {
        headers: {
          'Authorization': bearerToken,
        }
      }
    }
    return header;
  }

  public postUserCreate(user : User) : Observable<any> {
    return this.http.post(URL_SIGN, user)
  }

  public postUserConnect(user : User): Observable<any> {
    return this.http.post(URL_LOGIN, user)
  }

  public getUserInformation(): Observable<any> {
    let header = this.setHttpHeader();
    return this.http.get(URL_USER, header)
  }

  public deleteUser() {
    let header = this.setHttpHeader();
    return this.http.delete(URL_USER, header)
  }

  public updateUser(user : User): Observable<any> {
    let header = this.setHttpHeader();
    return this.http.put(URL_USER, user, header)
  }
}
