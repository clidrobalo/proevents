import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { UserService } from '@app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    /**
     *
     */
    constructor(private router: Router,
        private toasterService: ToastrService,
        private userService: UserService) {
    }

    canActivate(): boolean {
        if (this.userService.getToken() !== "")
            return true;

        this.toasterService.info("Invalid session. Please login again.", "Info");
        this.router.navigate(['/user/login']);
        return false;
    }

}
