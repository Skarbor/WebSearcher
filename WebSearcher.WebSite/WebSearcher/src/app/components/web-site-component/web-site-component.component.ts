import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: '[app-web-site-component]',
  templateUrl: './web-site-component.component.html',
  styleUrls: ['./web-site-component.component.less']
})
export class WebSiteComponentComponent implements OnInit {
  
  @Input() x: number;
  @Input() y: number;
  @Input() r: number;
  @Input() name: string;

  constructor() { }

  ngOnInit() {
  }


  getTextX() : number {
    return Number(this.x) - (Number(this.r) / 2);
  }

  getTextY() : number {
    return Number(this.y) + Number(this.r) + 15;
  }
}
