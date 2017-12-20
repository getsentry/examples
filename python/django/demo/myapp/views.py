# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.views.generic.base import TemplateView

# Create your views here.


class HomePageView(TemplateView):
    template_name = "myapp/home.html"


class GoodView(TemplateView):
    template_name = "myapp/good.html"


class BadView(TemplateView):
    template_name = "myapp/bad.html"

    def get_context_data(self, **kwargs):
        context = super(BadView, self).get_context_data(**kwargs)
        if True:
            raise Exception('Bad View Loaded')
        return context
