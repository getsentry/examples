from django.conf.urls import url

from . import views

urlpatterns = [
    url(r'^$', views.HomePageView.as_view(), name='index'),
    url(r'^good/$', views.GoodView.as_view(), name='good'),
    url(r'^bad/$', views.BadView.as_view(), name='bad'),
]
