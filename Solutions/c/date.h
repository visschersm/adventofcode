#ifndef DATE_H
#define DATE_H

typedef struct
{
    int day;
    int year;
} Date;

Date parseDate(const char *dateString);

#endif
