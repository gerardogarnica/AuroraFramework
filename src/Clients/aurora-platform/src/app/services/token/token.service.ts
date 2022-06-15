import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private tokenKeyName: string = 'token';

  constructor() { }

  setToken(token: string) {
    localStorage.setItem(this.tokenKeyName, token);
  }

  getToken() {
    const token = localStorage.getItem(this.tokenKeyName);
    return token;
  }

  removeToken() {
    localStorage.removeItem(this.tokenKeyName);
  }

  hasToken(): boolean {
    const token = localStorage.getItem(this.tokenKeyName);
    return token != null;
  }
}
