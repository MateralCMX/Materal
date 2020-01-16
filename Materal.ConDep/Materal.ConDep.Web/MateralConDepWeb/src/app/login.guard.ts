import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorityCommon } from './common/AuthorityCommon';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  public constructor(private router: Router) { }
  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = AuthorityCommon.getToken();
    if (token) { return true; }
    this.router.navigate(['/Login']);
    return false;
  }
}
