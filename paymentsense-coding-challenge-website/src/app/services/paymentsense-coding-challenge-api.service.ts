import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Country } from '../models/Country';

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {
  list: Country[]
  constructor(private httpClient: HttpClient) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get('https://localhost:44341/health', { responseType: 'text' });
  }

  public getCountries(): Observable<any> {
    return this.httpClient.get('https://localhost:44341/PaymentsenseCodingChallenge');
  }
  
}