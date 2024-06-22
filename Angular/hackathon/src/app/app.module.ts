import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { MsalModule, MsalService, MSAL_INSTANCE, MSAL_GUARD_CONFIG, MSAL_INTERCEPTOR_CONFIG, MsalGuardConfiguration, MsalInterceptorConfiguration } from '@azure/msal-angular';
import { PublicClientApplication, InteractionType, IPublicClientApplication } from '@azure/msal-browser';

function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: 'c8058d8f-4e01-406e-bfed-ff564e9ed847', // Replace with your Azure AD application client ID
      authority: 'https://login.microsoftonline.com/a6f9c9e8-8623-43f5-a543-74108fd93a01', // Replace with your Azure AD tenant ID
      redirectUri: 'http://localhost:4200', // Replace with your redirect URI
    }
  });
}

const guardConfig: MsalGuardConfiguration = {
  interactionType: InteractionType.Redirect,
  authRequest: {
    scopes: ['user.read']
  }
};

const interceptorConfig: MsalInterceptorConfiguration = {
  interactionType: InteractionType.Redirect,
  protectedResourceMap: new Map([
    ['https://graph.microsoft.com/v1.0/me', ['user.read']],
    ['https://management.azure.com/', ['https://management.azure.com/.default']]
  ])
};

@NgModule({
  declarations: [
    AppComponent,
    // other components
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MsalModule.forRoot(MSALInstanceFactory(), guardConfig, interceptorConfig),
  ],
  providers: [
    MsalService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
