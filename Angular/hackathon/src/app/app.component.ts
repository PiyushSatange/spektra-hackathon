import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AuthenticationResult, EventType } from '@azure/msal-browser';
import { AzureService } from './azure.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private http:HttpClient, private service:AzureService){}

  regions:any;
  storage:any;
  ngOnInit(): void {
    this.service.getRegion().subscribe((data) => {
      this.regions = data;
      console.log(this.regions);
    });
  }

  onSelect(location:any){
    this.service.getStorage(location).subscribe((data) => {
      this.storage = data;
      console.log(data);
    });
  }
}
