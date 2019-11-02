import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FindWebSiteConnectionsPageComponent } from 'src/app/pages/find-web-site-connections-page/find-web-site-connections-page.component';
import { BigMapConnectionsPageComponent } from 'src/app/pages/big-map-connections-page/big-map-connections-page.component';
import { AboutPageComponent } from 'src/app/pages/about-page/about-page.component';

const routes: Routes = [
  { path: 'web-searcher', component: FindWebSiteConnectionsPageComponent },
  { path: 'big-map', component: BigMapConnectionsPageComponent },
  { path: 'about', component: AboutPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
