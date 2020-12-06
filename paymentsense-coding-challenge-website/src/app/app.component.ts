import { Component } from '@angular/core';
import { PaymentsenseCodingChallengeApiService } from './services';
import { take } from 'rxjs/operators';
import { faThumbsUp, faThumbsDown } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public faThumbsUp = faThumbsUp;
  public faThumbsDown = faThumbsDown;
  public title = 'Paymentsense Coding Challenge!';
  public paymentsenseCodingChallengeApiIsActive = false;
  public paymentsenseCodingChallengeApiActiveIcon = this.faThumbsDown;
  public paymentsenseCodingChallengeApiActiveIconColour = 'red';
  countries : any;
  config: any;
  currentCountry = null;
  currentIndex = -1;
  page = 1;
  count = 0;
  pageSize = 10;

  constructor(private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService) {
    paymentsenseCodingChallengeApiService.getHealth().pipe(take(1))
    .subscribe(
      apiHealth => {
        this.paymentsenseCodingChallengeApiIsActive = apiHealth === 'Healthy';
        this.paymentsenseCodingChallengeApiActiveIcon = this.paymentsenseCodingChallengeApiIsActive
          ? this.faThumbsUp
          : this.faThumbsUp;
        this.paymentsenseCodingChallengeApiActiveIconColour = this.paymentsenseCodingChallengeApiIsActive
          ? 'green'
          : 'red';
      },
      _ => {
        this.paymentsenseCodingChallengeApiIsActive = false;
        this.paymentsenseCodingChallengeApiActiveIcon = this.faThumbsDown;
        this.paymentsenseCodingChallengeApiActiveIconColour = 'red';
      });

      this.retrieveCountries();
  }

  retrieveCountries() {
    const localCountries = localStorage.getItem('localCountries');

    if(localCountries === null)
    {
    this.paymentsenseCodingChallengeApiService.getCountries()
      .subscribe(
        response => {
          const { countries } = response;
          this.countries = countries;
          this.count = this.countries.length;
        },
        error => {
          console.log(error);
        });
      }
      else
      {
        this.countries = JSON.parse(localCountries);
        console.log(this.countries);
      }
  }
  
  handlePageChange(event) {
    this.page = event;
    this.retrieveCountries();
  }

  handlePageSizeChange(event) {
    this.pageSize = event.target.value;
    this.page = 1;
    this.retrieveCountries();
  }
  
  setActiveCountry(country: any, index: number) {
    this.currentCountry = country;
    this.currentIndex = index;
  }
}
