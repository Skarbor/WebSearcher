import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WebSiteComponentComponent } from './components/web-site-component/web-site-component.component';
import { ContainerComponent } from './components/container/container.component';
import { MenuComponent } from './components/menu/menu.component';
import { FindWebSiteConnectionsPageComponent } from './pages/find-web-site-connections-page/find-web-site-connections-page.component';
import { BigMapConnectionsPageComponent } from './pages/big-map-connections-page/big-map-connections-page.component';
import { AboutPageComponent } from './pages/about-page/about-page.component';

@NgModule({
  declarations: [
    AppComponent,
    WebSiteComponentComponent,
    ContainerComponent,
    MenuComponent,
    FindWebSiteConnectionsPageComponent,
    BigMapConnectionsPageComponent,
    AboutPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
