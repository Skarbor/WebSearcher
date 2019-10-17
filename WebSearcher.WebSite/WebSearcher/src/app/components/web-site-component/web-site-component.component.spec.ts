import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WebSiteComponentComponent } from './web-site-component.component';

describe('WebSiteComponentComponent', () => {
  let component: WebSiteComponentComponent;
  let fixture: ComponentFixture<WebSiteComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WebSiteComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebSiteComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
