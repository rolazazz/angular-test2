import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivityService } from '../activity.service';
import { Activity } from '../models/activity.model';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { MatTableDataSource } from '@angular/material/table';



@Component({
  selector: 'app-activity-server',
  templateUrl: './activity-server.component.html',
  styleUrls: ['./activity-server.component.css'],
})
export class ActivityServerComponent implements OnInit, OnDestroy {
  toppings = new FormControl('');

  private subs = new Subscription();

  public activities: Activity[] = [];
  //public dataSource!: MatTableDataSource<any>;
  public properties: string[] = ["project", "employee", "date", "hours"];
  public columns: string[] = [];

  public aggregations: string[] = ["project", "employee", "date"];
  public selectedAggregations: string[] = [];




  constructor(private activityService: ActivityService) {
  }

  //keep the selection order of the Mat-Select
  //https://stackoverflow.com/questions/74543480/angular-order-of-selected-option-in-multiple-mat-select
  noCompareFunction() {
    return 0;
  }

  ngOnInit(): void {
    this.getActivities();
  }

  ngOnDestroy() {
    if (this.subs) {
      this.subs.unsubscribe();
    }
  }

  getActivities(): void {
    this.subs.add(
      this.activityService.getActivities(this.selectedAggregations)
        .subscribe((res) => {
          //console.log(res);
          this.activities = res;
          //this.dataSource = new MatTableDataSource<Activity>(res);
          this.columns = (this.selectedAggregations.length == 0 ? this.properties : [...this.selectedAggregations, ...["hours"]]);
          console.log(this.selectedAggregations);
        },
          (err: HttpErrorResponse) => {
            console.log(err);
          })
    );

  }



}


