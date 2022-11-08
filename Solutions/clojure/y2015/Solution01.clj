(ns y2015.Solution01
  (:require [clojure.string :as str]))

(defn sign [c] 
  (case c 
    "(" 1
    ")" -1
    0))

(defn directions [data] (for [c data] (sign c)))
(defn Part1 [input_file]
  (def input (slurp input_file))
  (def data (str/split input #""))
  (def result (reduce + (directions data)))
  (println (str "Santa is on the " result "th floor"))
  )


(defn Part2 [input_file]
  (def input (slurp input_file))
  (def data (str/split input #""))
  (def dir (directions data))
  (def result (count (take-while some?
                     (reductions
                      #(if (= 1 -1) (reduced nil) (+ %1 %2))
                      dir))))
  (println (str "Santa is on the " result "th floor"))
  )

(defn Solve [input_file]
  (Part1 input_file)
  (Part2 input_file))



(Solve (nth *command-line-args* 2))