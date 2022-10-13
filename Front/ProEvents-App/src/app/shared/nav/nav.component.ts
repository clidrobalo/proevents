import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '@app/services/user.service';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
    isCollapsed = true;

    constructor(private route: Router,
        public userService: UserService) { }

    get username() {
        return this.userService.getUsername();
    }

    ngOnInit() {
    }

    isToShowNav(): boolean {
        return (this.route.url !== '/user/login') && (this.route.url !== '/user/registration');
    }

    public logout(): void {
        this.userService.logout();
    }

    public isUserLogouted(): boolean {
        return this.userService.getToken() === "" || this.userService.currentUser$ === null;
    }
}
