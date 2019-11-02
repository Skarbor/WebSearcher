import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FindWebSiteConnectionsPageComponent } from './find-web-site-connections-page.component';

describe('FindWebSiteConnectionsPageComponent', () => {
  let component: FindWebSiteConnectionsPageComponent;
  let fixture: ComponentFixture<FindWebSiteConnectionsPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FindWebSiteConnectionsPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FindWebSiteConnectionsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
