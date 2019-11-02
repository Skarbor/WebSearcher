import { Component, OnInit, AfterViewInit } from '@angular/core';
import * as d3 from 'd3';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.less']
})
export class ContainerComponent implements AfterViewInit {

  constructor() { }

  width: number = 1800;
  height: number = 1200;

  ngAfterViewInit() {
    this.newInit();
  }

  newInit() {
    // append the svg object to the body of the page
    var svg = d3.select(".container-component")
      .append("svg")
      .attr("width", this.width)
      .attr("height", this.height)
      .append("g")

    var webPageConnections;

    d3.json("https://localhost:44353/WebPageConnections").then(data => {
      webPageConnections = data;

      d3.json("https://localhost:44353/WebPages").then(data => {
        console.log(data);
        console.log(webPageConnections);

        var link = svg
          .selectAll("line")
          .data(webPageConnections)
          .enter()
          .append("line")
          .style("stroke", "#aaa")

        var node = svg
          .selectAll("circle")
          .data(data)
          .enter()
          .append("circle")
          .attr("r", 20)
          .style("fill", "#69b3a2");

        var text2 = svg
          .selectAll("text")
          .data(data)
          .enter()
          .append("text")
          .text(function (d) { return d.url });

        var simulation = d3.forceSimulation(data)                 // Force algorithm is applied to data.nodes
          .force("charge", d3.forceManyBody().strength(-50))         // This adds repulsion between nodes. Play with the -400 for the repulsion strength
          .force("center", d3.forceCenter(this.width / 2, this.height / 2))     // This force attracts nodes to the center of the svg area
          .on("end", ticked);


        function ticked() {
          link
            .attr("x1", function (d) { return getWebPageByIdX(d.webPageFrom.id) })
            .attr("y1", function (d) { return getWebPageByIdY(d.webPageFrom.id) })
            .attr("x2", function (d) { return getWebPageByIdX(d.webPageTo.id) })
            .attr("y2", function (d) { return getWebPageByIdY(d.webPageTo.id) })
            .style("stroke", function (d) { return getRandomColor() })
          // .attr("x1", function (d) { return data[0].x; })
          // .attr("y1", function (d) { return data[0].y; })
          // .attr("x2", function (d) { return data[1].x; })
          // .attr("y2", function (d) { return data[1].y; });

          node
            .attr("cx", function (d) { return d.x + 2; })
            .attr("cy", function (d) { return d.y - 2; });

          text2
            .attr("x", function (d) { return d.x + 2; })
            .attr("y", function (d) { return d.y - 2; });
        }

        function getRandomColor() {
          var letters = '0123456789ABCDEF';
          var color = '#';
          for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
          }
          return color;
        }

        function getWebPageByIdX(id): any {

          let x;
          data.forEach(element => {
            if (Number(element.id) === Number(id)) {
              console.log('x for element ' + id + ' :' + element.x);
              x = element.x;
            }
          });
          return x;
          console.log('no x for element ' + id);
        }

        function getWebPageByIdY(id): any {
          let y;
          data.forEach(element => {
            if (Number(element.id) === Number(id)) {
              console.log('y for element ' + id + ' :' + element.y);
              y = element.y;
            }
          });
          return y;
          console.log('no y for element ' + id);
        }
      });
    });
  }
}
