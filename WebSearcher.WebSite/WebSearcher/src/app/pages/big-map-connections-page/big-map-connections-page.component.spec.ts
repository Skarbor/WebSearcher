import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BigMapConnectionsPageComponent } from './big-map-connections-page.component';

describe('BigMapConnectionsPageComponent', () => {
  let component: BigMapConnectionsPageComponent;
  let fixture: ComponentFixture<BigMapConnectionsPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BigMapConnectionsPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BigMapConnectionsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
