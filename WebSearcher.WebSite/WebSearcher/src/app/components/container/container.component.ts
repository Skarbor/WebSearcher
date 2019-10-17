import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.less']
})
export class ContainerComponent implements OnInit {

  constructor() { }

  width: number = 1900;
  height: number = 900;

  ngOnInit() {
  }

}
