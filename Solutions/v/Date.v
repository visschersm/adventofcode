pub struct Date {
	year int
	day int
}

fn get_date(args []string) Date {
	splitted := args[1].split("/")
	y := splitted[0].int()
	d := splitted[1].int()
	return Date {
		year: y
		day: d
		}
}