(ns sentry-clojure-example.core
  (:require [sentry-clj.core :as sentry]))

(def dsn "your dsn")

(sentry/init! dsn)

(defn -main [& args]
  (let [x 1
        y 0]
    (try
      (/ x y)
      (catch Exception e
        (sentry/send-event {:throwable e})))))
