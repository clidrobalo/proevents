import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { UserService } from '@app/services/user.service';
import { User } from '@app/models/user/User';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(private userService: UserService) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        var currentUser!: User;

        this.userService.currentUser$.pipe(take(1)).subscribe((user: User) => {
            currentUser = user
        });

        // is logged
        if (currentUser !== null) {
            request = request.clone(
                {
                    setHeaders: {
                        Authorization: this.userService.getToken()
                    }
                }
            );
        }

        // request = request.clone({
        //     headers: request.headers.set(UserConstant.AUTHORIZATION_KEY, UserConstant.PREFIX_AUTH + this.token)
        //   });

        return next.handle(request);
    }
}
