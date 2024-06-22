import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';
import { Observable, from } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AzureService {

  constructor(private http:HttpClient){}

  getRegion(){
    return this.http.get('https://localhost:7237/api/AzureManagement/locations');
  }

  getStorage(location:string){
    return this.http.get(`https://localhost:7237/api/AzureManagement/Storage/${location}`);
  }
}
