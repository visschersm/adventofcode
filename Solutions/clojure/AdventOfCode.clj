(ns AdventOfCode
  (:require [clojure.string :as str]) 
  )

(defn get_date [value]
  (str/split value #"/"))

(defn get_inputfile [date]
  (str "Inputs/" (get date 0) "/" (get date 1) ".txt" ))

(defn get_solution [date]
  (str "y" (get date 0) "/Solution" (get date 1) ".clj"))

(defn ARGV [n] (nth *command-line-args* n))

(defn run []  
  ;; (:require '[clojure.string :as str])
  (def date (get_date (ARGV 0)))
  (def input_file (get_inputfile date))
  (def solutionName (get_solution date))
  (println (str "InputFile: " input_file))
  (println (str "Solution: " solutionName))
  (load-file "/y2015/Solution01.clj")
  (:use '(y2015.Solution01 :as solution) (solution/Part1 input_file))
  ;; (solution/Part2)
  )

(run)