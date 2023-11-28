import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Activity } from './models/activity.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  /** GET activities from the server */
  getActivities(aggregations?: string[]): Observable<Activity[]> {

    const options = aggregations?.length ?
      { params: new HttpParams().set('aggregations', aggregations.join(',')) } : {};

    return this.http.get<Activity[]>(this.baseUrl + 'activities', options)
  }
}
