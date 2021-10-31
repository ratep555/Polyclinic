import { Injectable } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';
import { User } from 'src/app/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class IsAdminGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
      // here we dont have to subscribe, router will do that for us
    return this.accountService.currentUser$.pipe(
      map(auth => {
        if (auth.roles === ['Admin']) {
          return true;
        }
        // this means we are not currently loggedin
        this.router.navigate(['**']);
      })
    );
  }
}









