import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginationResult } from '@app/models/Pagination';
import { environment } from '@environments/environment';
import { catchError, map, Observable, take } from 'rxjs';
import { Event } from '../models/Event';

@Injectable({
    providedIn: 'root'
})
export class EventService {
    private _baseURL = environment.apiURL + '/api/events';

    constructor(private _http: HttpClient) { }

    public getEvents(page?: number, itemsPerPage?: number, term?: string): Observable<PaginationResult<Event[]>> {
        const paginatedResult = {} as PaginationResult<Event[]>;

        let params = new HttpParams;

        if (page != null && itemsPerPage != null) {
            params = params.append('pageNumber', page.toString());
            params = params.append('pageSize', itemsPerPage.toString());
        }

        if (term != null && term != '')
            params = params.append('term', term)

        return this._http
            .get<Event[]>(this._baseURL, { observe: 'response', params })
            .pipe(
                take(1),
                map((response) => {
                    if (response.body)
                        paginatedResult.result = response.body;

                    if (response.headers.has('Pagination')) {
                        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '{}');
                    }
                    return paginatedResult;
                }));
    }


    public getEventById(id: number): Observable<Event> {
        return this._http.get<Event>(`${this._baseURL}/${id}`).pipe(take(1));
    }

    public addEvent(event: Event): Observable<Event> {
        return this._http.post<Event>(this._baseURL, event).pipe(take(1));
    }

    public updateEvent(event: Event): Observable<Event> {
        return this._http.put<Event>(this._baseURL, event).pipe(take(1));
    }

    public deleteEventById(id: number) {
        return this._http.delete(`${this._baseURL}/${id}`, { responseType: 'text' }).pipe(take(1));
    }

    public uploadImage(eventId: number, file: File): Observable<Event> {
        const fileToUpload = file as File;
        const formData = new FormData();
        formData.append('file', fileToUpload);

        return this._http.post<Event>(`${this._baseURL}/upload-image/${eventId}`, formData).pipe(take(1));
    }

    public loadImage(imageURL: string): any {
        return imageURL !== '' ? environment.apiURL + '/resources/images/' + imageURL : 'assets/img/no-photo.png';;
    }
}
