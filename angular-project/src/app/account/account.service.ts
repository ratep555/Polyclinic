import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IForgotPassword } from '../shared/models/forgotpassword';
import { IGender } from '../shared/models/gender';
import { MultipleSelectorModel } from '../shared/models/multiple-selector.model';
import { IResetPassword } from '../shared/models/resetpassword';
import { ISpecialization } from '../shared/models/specialization';
import { User } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http: HttpClient, private router: Router) { }

  login(values: any) {
    return this.http.post(this.baseUrl + 'account/login', values)
     .pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  registerPatient(formData) {
    return this.http.post(this.baseUrl + 'account/register', formData).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }



  // ovo je originalna verzija koja šljaka
  registerDoctor(values: any) {
    return this.http.post(this.baseUrl + 'account/registerdoctor1', values).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  // ova dva dolje su pokušaj za multipleselectormodel
  registerDoctor2(values: any) {
    return this.http.post(this.baseUrl + 'account/registerdoctor2', values).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  forgotPassword(forgotpassword: IForgotPassword) {
    return this.http.post(this.baseUrl + 'account/forgotpassword', forgotpassword);
  }

  resetPassword(resetpassword: IResetPassword) {
    return this.http.post(this.baseUrl + 'account/resetpassword', resetpassword);
  }

  getSpecializations() {
    return this.http.get<ISpecialization[]>(this.baseUrl + 'doctors1/multiplemodel');
  }

  getGenders() {
    return this.http.get<IGender[]>(this.baseUrl + 'patients1/genders');
  }



  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  // ovo nadalje je felipe vježba!

getFieldFromJWT(field: string): string {
  const token = localStorage.getItem('token');
  if (!token){return ''; }
  // we are parsing the data of the token
  // we are getting the second part of the token, [1], token ima tri dijela sjeti se!
  // the second part of the token is payload where the claims are
  // atob is decoding string
  const dataToken = JSON.parse(atob(token.split('.')[1]));
  return dataToken[field];
}

getRole(): string {
  return this.getFieldFromJWT('role');
}

 /*  isAuthenticated(): boolean {
    const user = localStorage.getItem('user');
    if (!user){
      return false;
    }
    return true;
  } */



}










