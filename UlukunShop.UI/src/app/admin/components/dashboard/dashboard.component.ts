import {Component, OnInit, ViewChild} from '@angular/core';
import {BaseComponent, SpinnerType} from "../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";
import {AlertifyService, MessageType, Position} from "../../../services/admin/alertify.service";
import {SignalrService} from "../../../services/common/signalr.service";
import {ReceiveFunctions} from "../../../constants/receive-functions";
import {HubUrls} from "../../../constants/hub-urls";
import {ApexAxisChartSeries, ApexChart, ApexDataLabels, ApexGrid, ApexStroke,
  ApexTitleSubtitle, ApexXAxis, ChartComponent} from "ng-apexcharts";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  constructor( _spinner:NgxSpinnerService, private _alertify:AlertifyService,private signalrService:SignalrService) {
    super(_spinner)
    this.chartOptions = {
      series: [
        {
          name: "Desktops",
          data: [10, 41, 35, 51, 49, 62, 69, 91, 148,,,,]
        }
      ],
      chart: {
        height: 350,
        type: "line",
        zoom: {
          enabled: true
        }
      },
      dataLabels: {
        enabled: true
      },
      stroke: {
        curve: "straight"
      },
      title: {
        text: "Aylara Göre Satışlar",
        align: "left"
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
          opacity: 0.5
        }
      },
      xaxis: {
        categories: [
          "Ocak",
          "Şubat",
          "Mart",
          "Nisan",
          "Mayıs",
          "Haziran",
          "Temmuz",
          "Ağustos",
          "Eylül",
          "Ekim",
          "Kasım",
          "Aralık"
        ]
      }
    };
  }

  ngOnInit(): void {
    this.signalrService.on(HubUrls.ProductHub,ReceiveFunctions.ProductAddedMessageReceiveFunction, message => {
      this._alertify.message(message, {
        messageType: MessageType.Message,
        position: Position.TopCenter
      })
    });
    this.signalrService.on(HubUrls.OrderHub,ReceiveFunctions.OrderAddedMessageReceiveFunction, message => {
      this._alertify.message(message, {
        messageType: MessageType.Notify,
        position: Position.TopCenter
      })
    });
  this.showSpinner(SpinnerType.Triangle)

  }

}

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  grid: ApexGrid;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
};
