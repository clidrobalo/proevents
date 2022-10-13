import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '@app/models/user/Login';
import { User } from '@app/models/user/User';
import { environment } from '@environments/environment';
import { stringify } from 'querystring';
import { map, Observable, ReplaySubject, take } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private currentUserSource = new ReplaySubject<User>(1);
    public currentUser$ = this.currentUserSource.asObservable();

    private _baseURL = environment.apiURL + '/api/user';

    constructor(private _http: HttpClient) { }

    public register(user: User): Observable<any> {
        return this._http.post<User>(`${this._baseURL}/register`, user).pipe(take(1));
    }

    public login(login: Login): Observable<any> {
        return this._http.post<User>(`${this._baseURL}/login`, login)
            .pipe(take(1),
                map((user: User) => {
                    if (user) {
                        this.setCurrentUser(user);
                    }
                }));
    }

    public logout(): void {
        localStorage.removeItem('user');
        this.currentUserSource.next({} as User);
        this.currentUserSource.complete();
    }

    public getToken(): string {
        var user = localStorage.getItem("user");

        if (user !== null) {
            return "Bearer " + JSON.parse(user).token;
        }
        return "";
    }

    public getUsername(): string {
        var user = localStorage.getItem("user");

        if (user !== null) {
            return JSON.parse(user).username;
        }
        return "";
    }

    private setCurrentUser(user: User): void {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
    }
}
