import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  public count = 5;
  public refresh(): void {
    const params = new HttpParams().set('count', this.count.toString());
    this.http.get<WeatherForecast[]>(this.baseUrl + 'api/weatherforecast', {params: params}).subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.refresh();
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
